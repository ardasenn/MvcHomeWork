using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MediumClone.Repositories.Abstract
{
    public interface IArticleRepository: IRepository<Article>
    {
        IEnumerable<Article> GetAllIncludeAuthors();
    }
}
