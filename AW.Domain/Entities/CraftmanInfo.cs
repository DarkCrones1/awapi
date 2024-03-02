using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Craftman
{
    public string? History { get; set; }

    public short? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }
}