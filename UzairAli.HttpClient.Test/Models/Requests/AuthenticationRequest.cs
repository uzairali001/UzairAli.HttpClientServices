using System;

namespace UzairAli.HttpClient.Test.Models.Requests;
internal class AuthenticationRequest
{
    public Guid AuthUuid { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public ApplicationDetailRequest ApplicationDetails { get; set; } = new ApplicationDetailRequest();
}
