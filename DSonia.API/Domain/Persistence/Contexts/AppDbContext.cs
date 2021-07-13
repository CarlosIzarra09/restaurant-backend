using DSonia.API.Domain.Models;
using DSonia.API.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Moneylender> Moneylenders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PayMoneylender> PayMoneylenders { get; set; }
        public DbSet<PaySupplier> PaySuppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //User
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();

            //Attendance
            builder.Entity<Attendance>().ToTable("Attendances");
            builder.Entity<Attendance>().HasKey(p => p.Id);
            builder.Entity<Attendance>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Attendance>().Property(p => p.EntryTime).IsRequired();
            builder.Entity<Attendance>().Property(p => p.OutTime).IsRequired();
            builder.Entity<Attendance>().Property(p => p.AttendanceDate).IsRequired();
            //Relationships
            builder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId);

            //Salary
            builder.Entity<Salary>().ToTable("Salaries");
            builder.Entity<Salary>().HasKey(p => p.Id);
            builder.Entity<Salary>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Salary>().Property(p => p.mount).IsRequired();
            builder.Entity<Salary>().Property(p => p.PayDate).IsRequired();
            //Relationships
            builder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeId);

            //Employee
            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.Phone);
            builder.Entity<Employee>().Property(p => p.IsShipper).IsRequired();
            //Relationships
            builder.Entity<Employee>()
                .HasMany(p => p.Salaries)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);
            builder.Entity<Employee>()
                .HasMany(p => p.Attendances)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);
            builder.Entity<Employee>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);

            //Order
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(p => p.Id);
            builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(p => p.RequiredTime).IsRequired();
            builder.Entity<Order>().Property(p => p.ShipTime).IsRequired();
            builder.Entity<Order>().Property(p => p.ShipDate).IsRequired();
            builder.Entity<Order>().Property(p => p.ShipAddress).IsRequired();
            builder.Entity<Order>().Property(p => p.ShipPrice).IsRequired();
            builder.Entity<Order>().Property(p => p.Debt).IsRequired();
            builder.Entity<Order>().Property(p => p.Description).IsRequired();
            //Relationships
            builder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId);
            builder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.ClientId);
            builder.Entity<Order>()
                .HasOne(o => o.PaymentMethod)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.PaymentMethodId);
            builder.Entity<Order>()
                .HasMany(p => p.OrderDetails)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            //Client
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Client>().HasKey(p => p.Id);
            builder.Entity<Client>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Client>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Client>().Property(p => p.Phone).IsRequired();
            builder.Entity<Client>().Property(p => p.Address).IsRequired();
            builder.Entity<Client>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            //Payment method
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            builder.Entity<PaymentMethod>().HasKey(p => p.Id);
            builder.Entity<PaymentMethod>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<PaymentMethod>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.PaymentMethod)
                .HasForeignKey(p => p.PaymentMethodId);

            //Order details
            builder.Entity<OrderDetail>().ToTable("OrderDetails");
            builder.Entity<OrderDetail>().HasKey(p => p.Id);
            builder.Entity<OrderDetail>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<OrderDetail>()
                .HasOne(o => o.Product)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(o => o.ProductId);
            builder.Entity<OrderDetail>()
               .HasOne(o => o.Order)
               .WithMany(e => e.OrderDetails)
               .HasForeignKey(o => o.OrderId);

            //Products
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.UnitPrice).IsRequired();
            builder.Entity<Product>().Property(p => p.UnitInStock).IsRequired();
            builder.Entity<Product>()
               .HasOne(o => o.Category)
               .WithMany(e => e.Products)
               .HasForeignKey(o => o.CategoryId);
            builder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            //Categories
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            //Suppliers
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<Supplier>().HasKey(p => p.Id);
            builder.Entity<Supplier>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Supplier>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Supplier>().Property(p => p.Phone);
            builder.Entity<Supplier>()
                .HasMany(p => p.PaySuppliers)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            //PaySupplier
            builder.Entity<PaySupplier>().ToTable("PaySuppliers");
            builder.Entity<PaySupplier>().HasKey(p => p.Id);
            builder.Entity<PaySupplier>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaySupplier>().Property(p => p.PayDate).IsRequired();
            builder.Entity<PaySupplier>().Property(p => p.Comment);
            builder.Entity<PaySupplier>()
               .HasOne(o => o.Supplier)
               .WithMany(e => e.PaySuppliers)
               .HasForeignKey(o => o.SupplierId);

            //Moneylender
            builder.Entity<Moneylender>().ToTable("Moneylenders");
            builder.Entity<Moneylender>().HasKey(p => p.Id);
            builder.Entity<Moneylender>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Moneylender>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Moneylender>().Property(p => p.Phone);
            builder.Entity<Moneylender>().Property(p => p.DebtDate).IsRequired();
            builder.Entity<Moneylender>().Property(p => p.DebtTotal).IsRequired();
            builder.Entity<Moneylender>()
                .HasMany(p => p.PayMoneylenders)
                .WithOne(p => p.Moneylender)
                .HasForeignKey(p => p.MoneylenderId);

            //PayMoneylender
            builder.Entity<PayMoneylender>().ToTable("PayMoneylenders");
            builder.Entity<PayMoneylender>().HasKey(p => p.Id);
            builder.Entity<PayMoneylender>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PayMoneylender>().Property(p => p.Amortize).IsRequired();
            builder.Entity<PayMoneylender>().Property(p => p.PayDate).IsRequired();
            builder.Entity<PayMoneylender>().Property(p => p.PayTime).IsRequired();
            builder.Entity<PayMoneylender>()
               .HasOne(o => o.Moneylender)
               .WithMany(e => e.PayMoneylenders)
               .HasForeignKey(o => o.MoneylenderId);



            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
