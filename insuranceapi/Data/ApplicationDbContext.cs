﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class ApplicationDbContext : DbContext {
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ClaimType> ClaimTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
