using MedicalResearch.Enums;

namespace MedicalResearch
{
	public class CommandResult
	{
		public int Code { get; }
		public string? Description { get; }

		public bool Succeeded
		{
			get
			{
				return (Code == 0);
			}
		}

		public CommandResult(int code)
		{
			Code = code;
		}

		public CommandResult(int code, string description)
		{
			Code = code;
			Description = description;
		}

		public CommandResult(CommandResults result)
		{
			Code = (int)result;
			Description = result.ToString().Replace('_', ' ');
		}
	}
}
