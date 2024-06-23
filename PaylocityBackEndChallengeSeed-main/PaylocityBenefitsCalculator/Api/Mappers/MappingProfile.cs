using System;
using System.Net.NetworkInformation;
using Api.CommandQueryImp.Commands;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mappers
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{ 
			CreateMap<Employee, GetEmployeeDto>()
				.AfterMap((dest, src) =>
				{
					src.Dependents = (src.Dependents != null && src.Dependents.Any()) ? src.Dependents : null;
				})
				.PreserveReferences();
			CreateMap<Dependent, GetDependentDto>()
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => Enum.Parse(typeof(Relationship), src.Relationship.ToString(), true)))
                .PreserveReferences();
			CreateMap<Dependent, CreateDependentCommand>();
            CreateMap<CreateDependentDto, CreateDependentCommand>();
			CreateMap<GetDependentDto, CreateDependentCommand>();


        }
    }
}

