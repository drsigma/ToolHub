public class CreateScheduleViewModel
{
    public string Name { get; set; }
    public List<User> ExistingUsers { get; set; } = new();
    public Guid SelectedUserId { get; set; }
    public Schedule? SelectedUserSchedule { get; set; } 
}
