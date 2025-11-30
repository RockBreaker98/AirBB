using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer
{
    public class QueryOptions<T> where T : class
    {
        // WHERE clause
        public Expression<Func<T, bool>>? Where { get; set; }

        // ORDER BY
        public Expression<Func<T, object>>? OrderBy { get; set; }

        // INCLUDE navigation properties (strings like "Location", "Owner")
        public List<string> Includes { get; } = new List<string>();

        public void Include(string navigation)
        {
            if (!string.IsNullOrEmpty(navigation) && !Includes.Contains(navigation))
            {
                Includes.Add(navigation);
            }
        }

        // Helper: apply query options to an IQueryable<T>
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            // Includes
            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            // Where
            if (Where != null)
            {
                query = query.Where(Where);
            }

            // OrderBy
            if (OrderBy != null)
            {
                query = query.OrderBy(OrderBy);
            }

            return query;
        }
    }
}