using System;

using System;

namespace Entities.Abstract
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
