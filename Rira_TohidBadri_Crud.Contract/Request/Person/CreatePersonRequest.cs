
namespace Rira_TohidBadri_Crud.Contract.Request.Person;

public record CreatePersonRequest(string FirstName, string LastName, string NationalCode, DateTime DateOfBirth);