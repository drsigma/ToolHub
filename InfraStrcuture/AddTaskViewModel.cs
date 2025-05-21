public class AddTaskViewModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateFinish { get; set; }
}
