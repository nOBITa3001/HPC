using FluentAssertions;
using FluentValidation.Results;
using HPC.Application.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HPC.Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Test]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Code", "must contain at 12 characters"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "Code" });
            actual["Code"].Should().BeEquivalentTo(new string[] { "must contain at 12 characters" });
        }

        [Test]
        public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Code", "must contain at 12 characters"),
                new ValidationFailure("Code", "must be 'AAAA-1111-A1' format"),
                new ValidationFailure("LengthInMetres", "must be greater than 0"),
                new ValidationFailure("WidthInMetres", "must be greater than 0"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "Code", "LengthInMetres", "WidthInMetres" });
            actual["Code"].Should().BeEquivalentTo(new string[] 
            {
                "must contain at 12 characters",
                "must be 'AAAA-1111-A1' format",
            });
            actual["LengthInMetres"].Should().BeEquivalentTo(new string[] { "must be greater than 0" });
            actual["WidthInMetres"].Should().BeEquivalentTo(new string[] { "must be greater than 0" });
        }
    }
}
