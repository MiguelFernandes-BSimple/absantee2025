using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Tests.AssociationCollabTrainingModuleTests
{
    public class AssociationCollabTrainingModuleConstructorTests
    {
        [Fact]
        public void WhenPassingValidData_ThenCreatesAssociation(){
            // arrange

            // act
            new AssociationCollabTrainingModule(1,2,3);

            // assert
        }

        [Fact]
        public void WhenPassingValidData_ThenCreateAssociation(){
            // arrange

            // act
            new AssociationCollabTrainingModule(2,3);

            // assert
        }
    }
}