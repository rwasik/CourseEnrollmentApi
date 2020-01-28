using CourseEnrollmentApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourseEnrollmentApi.DataAccess.Context
{
    public class DataContext : DbContext
    {
        private IConfiguration _config;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration config)
            : base(options) 
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<User>()
                .Property(n => n.Email)
                .IsRequired();                

            modelBuilder.Entity<Course>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Course>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Course>()
                .Property(n => n.Name)
                .IsRequired();

            modelBuilder.Entity<UserCourse>()
                .HasKey(n => new { n.UserId, n.CourseId });

            modelBuilder.Entity<UserCourse>()
                .HasOne(n => n.User)
                .WithMany(n => n.UserCourses)
                .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<UserCourse>()
                .HasOne(n => n.Course)
                .WithMany(n => n.UserCourses)
                .HasForeignKey(n => n.CourseId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:CourseEnrollmentDataContext"]);
        }
    }
}