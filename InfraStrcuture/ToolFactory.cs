using System;
using System.Collections.Generic;

public static class ToolFactory
{
    public static Tool CreateDefault(string name)
    {
        return new Tool
        {
            Id = Guid.NewGuid(),
            Name = name,
            Topics = new List<Topics>
                {
                    new Topics
                    {
                        Id = Guid.NewGuid()

                    }

            }
        };
    }
}
