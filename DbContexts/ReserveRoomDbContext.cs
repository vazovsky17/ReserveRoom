using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserveRoom.DTOs;
using ReserveRoom.Models;

namespace ReserveRoom.DbContexts
{
    public class ReserveRoomDbContext : DbContext
    {
        public ReserveRoomDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
