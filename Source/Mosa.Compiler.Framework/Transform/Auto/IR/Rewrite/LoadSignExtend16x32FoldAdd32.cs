// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework.IR;

namespace Mosa.Compiler.Framework.Transform.Auto.IR.Rewrite
{
	/// <summary>
	/// LoadSignExtend16x32FoldAdd32
	/// </summary>
	public sealed class LoadSignExtend16x32FoldAdd32 : BaseTransformation
	{
		public LoadSignExtend16x32FoldAdd32() : base(IRInstruction.LoadSignExtend16x32)
		{
		}

		public override bool Match(Context context, TransformContext transformContext)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.Add32)
				return false;

			if (!IsResolvedConstant(context.Operand2))
				return false;

			if (!IsResolvedConstant(context.Operand1.Definitions[0].Operand2))
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transformContext)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand1;
			var t2 = context.Operand1.Definitions[0].Operand2;
			var t3 = context.Operand2;

			var e1 = transformContext.CreateConstant(Add32(To32(t2), To32(t3)));

			context.SetInstruction(IRInstruction.LoadSignExtend16x32, result, t1, e1);
		}
	}
}
