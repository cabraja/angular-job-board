namespace API.Helpers.DTO
{
    public class CreateEmployerSimpleDTO
    {
        public string EmployerName { get; set; }
    }
    public class CreateEmployerDTO : CreateEmployerSimpleDTO
    {
        public string AppUserId { get; set; }
    }
}
