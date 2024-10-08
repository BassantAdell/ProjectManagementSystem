﻿using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<Result<ProjectDto>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    //public ICollection<Guid> UserIds { get; set; }  // Assuming user IDs are used for updates
}

