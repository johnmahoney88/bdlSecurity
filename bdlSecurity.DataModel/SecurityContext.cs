using Microsoft.Data.Entity;

namespace bdlSecurity.DataModel
{


    public class SecurityContext : DbContext
    {
        string _connection;
        public SecurityContext(string connection)
            : base()
        {
            _connection = connection;
        }

        public virtual DbSet<NoFlySelectee> NoFlySelectee { get; set; }
        public virtual DbSet<tb_badge> tb_badge { get; set; }
        public virtual DbSet<tb_request> tb_request { get; set; }
        public virtual DbSet<tb_user> tb_user{ get; set; }
        public virtual DbSet<tb_request_escort> tb_request_escort { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
            optionsBuilder.UseSqlServer(_connection);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_user>().HasKey(b => b.b_number_str);
            modelBuilder.Entity<tb_badge>().HasKey(b => b.b_number_str);
            modelBuilder.Entity<tb_request>().HasKey(r => r.RequestID);
            modelBuilder.Entity<tb_request_escort>().HasKey(k => new { k.RequestorBadgeNumber, k.RequestID, k.EscortNo });

            modelBuilder.Entity<tb_request_escort>().HasOne(e => e.Request).WithMany(r => r.RequestEscorts).IsRequired();                

            base.OnModelCreating(modelBuilder);
        }
    }
}
