using AutoMapper;
using HPC.Application.Common.Mappings;
using HPC.Application.Dtos.Ships;
using HPC.Domain.Entities;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace HPC.Application.UnitTests.Common.Mappings
{
    public class MappingProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration() => _configuration.AssertConfigurationIsValid();

        [Test]
        [TestCase(typeof(Ship), typeof(ShipDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            static object GetInstanceOf(Type type)
            {
                if (type.GetConstructor(Type.EmptyTypes) != null)
                    return Activator.CreateInstance(type);

                return FormatterServices.GetUninitializedObject(type);
            }

            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
