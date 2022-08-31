namespace EmployeeSample.Application.Interfaces.Commands;
public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResult
{
    Task<TResult> Handle(TCommand command);
}
