using ITS.BiblioAccess.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Presentation.Dtos;

public record CareerDTO(Guid Id, string Name, bool IsActive)
{
    public static CareerDTO FromCareer(Career career)
    {
        return new CareerDTO
        (
            career.CareerId,
            career.Name.Value,
            career.IsActive
        );
    }
}
