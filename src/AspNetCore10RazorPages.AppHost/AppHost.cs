var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AspNetCore10RazorPages>("aspnetcore10razorpages");

builder.Build().Run();
