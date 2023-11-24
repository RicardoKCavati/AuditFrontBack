using AuditApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditApp.Server.Database
{
    public class AuditContext : DbContext
    {
        //Add-Migration EmptyDatabase
        //Update-Database
        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasKey(address => new { address.AddressId});
            modelBuilder.Entity<CompanyModel>().HasKey(company => new { company.CompanyId});
            modelBuilder.Entity<UserModel>().HasKey(user => new { user.UserId });
            modelBuilder.Entity<QuestionDb>().HasKey(question => new { question.QuestionId });
            modelBuilder.Entity<AnswerModel>().HasKey(answer => new { answer.QuestionId, answer.CompanyId });
            
        }

        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<QuestionDb> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
