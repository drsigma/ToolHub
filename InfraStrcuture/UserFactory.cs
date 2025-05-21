using System;
using System.Collections.Generic;

public static class UserFactory
{
    public static User CreateDefault(string name)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
                Tasks = new List<Tasks>
                {
                    new Tasks
                    {
                        Id = Guid.NewGuid(),
                        Title = "Get Started",
                        Description = "Your first task!",
                        DateStart = DateTime.Today.AddDays(1),
                        DateFinish= DateTime.Today.AddDays(1)

                    }
     
            }
        };
    }
}
