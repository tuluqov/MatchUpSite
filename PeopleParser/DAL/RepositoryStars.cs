using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace PeopleParser.DAL
{
    public class RepositoryStars
    {
        //protected readonly DbContext Context;
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<Star> Items;

        public RepositoryStars()
        {
            Context = new ApplicationDbContext();
            Items = Context.Stars;
        }

        public void Create(Star item)
        {
            if (!Items.Any(x => x.Name == item.Name))
            {
                Items.Add(item);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Add: {item.Name}");
                Console.ResetColor(); // сбрасываем в стандартный
            }
        }

        public IEnumerable<Star> Find(Func<Star, bool> predicate)
        {
            IEnumerable<Star> result = Items.Where(predicate).ToList();
            return result;
        }

        public IEnumerable<Star> GetAll()
        {
            return Items.ToList();
        }

        public Star GetById(object id)
        {
            Star resault = Items.Find(id);
            return resault;

        }
        
    }
}