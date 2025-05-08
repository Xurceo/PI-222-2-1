using DAL;
using Microsoft.EntityFrameworkCore;

var context = new AuctionDbContext();
context.Database.Migrate();