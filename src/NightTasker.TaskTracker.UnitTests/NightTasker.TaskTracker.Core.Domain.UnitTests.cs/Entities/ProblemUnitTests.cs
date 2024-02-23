using Bogus;
using FluentAssertions;
using NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Problems;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Domain.UnitTests.cs.Entities;

public class ProblemUnitTests
{
    private Faker _faker = null!;

    [SetUp]
    public void Setup()
    {
        _faker = new Faker();
    }

    [Test]
    public void CreateInstance__ProblemIsOpen()
    {
        // act
        var problem = Problem.CreateInstance(Guid.NewGuid(), Guid.NewGuid(), _faker.Random.AlphaNumeric(10));
        
        // assert
        problem.Status.Should().Be(ProblemStatus.Open);
    }
    
    [TestCase(ProblemStatus.InProgress)]
    [TestCase(ProblemStatus.UnderReview)]
    [TestCase(ProblemStatus.Resolved)]
    [TestCase(ProblemStatus.Closed)]
    public void OpenProblem_ProblemHasAnotherInitialStatus_ProblemStatusIsOpen(ProblemStatus initialProblemStatus)
    {
        // arrange
        var problem = Problem.CreateInstance(Guid.NewGuid(), Guid.NewGuid(), _faker.Random.AlphaNumeric(10));
        SetStatusForProblem(problem, initialProblemStatus);
        
        // act
        problem.OpenProblem();
        
        // assert
        problem.Status.Should().Be(ProblemStatus.Open);
    }

    [Test]
    public void WorkOnProblem_ProblemHasNotAssignee_ThrowsNotAssignedProblemCanNotBeInProgress()
    {
        // arrange
        var problem = Problem.CreateInstance(Guid.NewGuid(), Guid.NewGuid(), _faker.Random.AlphaNumeric(10));
        var expectedException = new NotAssignedProblemCanNotBeInProgress(problem.Id);
        
        // act & assert
        var act = () => problem.WorkOnProblem();
        act.Should().Throw<NotAssignedProblemCanNotBeInProgress>().WithMessage(expectedException.Message);
    }

    [Test]
    public void ReviewProblem_ProblemIsInProgress_ProblemIsUnderReview()
    {
        // arrange
        var problem = Problem.CreateInstance(Guid.NewGuid(), Guid.NewGuid(), _faker.Random.AlphaNumeric(10));
        SetStatusForProblem(problem, ProblemStatus.InProgress);
        
        // act
        problem.ReviewProblem();
        
        // assert
        problem.Status.Should().Be(ProblemStatus.UnderReview);
    }
    
    [TestCase(ProblemStatus.Open)]
    [TestCase(ProblemStatus.UnderReview)]
    [TestCase(ProblemStatus.Resolved)]
    [TestCase(ProblemStatus.Closed)]
    public void ReviewProblem_ProblemIsNotInProgress_ThrowsNotInProgressProblemCanNotBeUnderReview(
        ProblemStatus problemStatus)
    {
        // arrange
        var problem = Problem.CreateInstance(Guid.NewGuid(), Guid.NewGuid(), _faker.Random.AlphaNumeric(10));
        SetStatusForProblem(problem, problemStatus);
        
        // act & assert
        var act = () => problem.ReviewProblem();
        act.Should().Throw<NotInProgressProblemCanNotBeUnderReview>();
    }

    private void SetStatusForProblem(Problem problem, ProblemStatus problemStatus)
    {
        switch (problemStatus)
        {
            case ProblemStatus.Open:
                problem.OpenProblem();
                break;
            case ProblemStatus.InProgress:
                problem.AssignProblem(Guid.NewGuid());
                problem.WorkOnProblem();
                break;
            case ProblemStatus.UnderReview:
                problem.ReviewProblem();
                break;
            case ProblemStatus.Resolved:
                problem.ResolveProblem();
                break;
            case ProblemStatus.Closed:
                problem.CloseProblem();
                break;
        }
    }
}