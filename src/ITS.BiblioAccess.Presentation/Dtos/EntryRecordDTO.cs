using ITS.BiblioAccess.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Presentation.Dtos;

public record EntryRecordDto(Guid EntryId,DateTime Timestamp,int UserType, Guid? CareerId,int Gender)
{ 

    public static EntryRecordDto FromEntryRecord(EntryRecord entry)
    {
        return new EntryRecordDto(
            entry.EntryId,
            entry.Timestamp,
            (int)entry.UserType, 
            entry.CareerId,
            (int)entry.Gender 
        );
    }
}

