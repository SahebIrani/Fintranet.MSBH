using Data.Shared.Constants;

using Domain.Common;
using Domain.Entities;

namespace Data.Shared.Extensions;

public static class TableMappingName
{
    public static TableName[] GetNames()
    {
        var names = new TableName[]
        {
            new TableName(nameof(Customer),TableMappingNameConst.PluralName.Customers,TableMappingNameConst.SchemaName.Mc2),
        };

        return names;
    }
}
