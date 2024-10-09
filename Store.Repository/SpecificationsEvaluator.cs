using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public static class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //Create And Return
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));




            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }


            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);

            }



            return query;

        }


    }
}
