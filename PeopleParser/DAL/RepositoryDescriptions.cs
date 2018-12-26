using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace PeopleParser.DAL
{
    public class RepositoryDescriptions
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<Description> Items;

        public RepositoryDescriptions()
        {
            Context = new ApplicationDbContext();
            Items = Context.Descriptions;
        }

        public void Create(Description item)
        {
            if (!Items.Any(x => x.Details == item.Details && x.Numbers == item.Numbers && x.Name == item.Name))
            {
                Items.Add(item);
            }
        }
        
        public IEnumerable<Description> Find(Func<Description, bool> predicate)
        {
            IEnumerable<Description> result = Items.Where(predicate).ToList();
            return result;
        }

        public IEnumerable<Description> GetAll()
        {
            return Items.ToList();
        }

        public Description GetById(object id)
        {
            Description resault = Items.Find(id);
            return resault;

        }

    }
}