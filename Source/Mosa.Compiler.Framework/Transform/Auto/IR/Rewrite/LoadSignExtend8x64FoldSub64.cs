// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework.IR;

namespace Mosa.Compiler.Framework.Transform.Auto.IR.Rewrite
{
	/// <summary>
	/// LoadSignExtend8x64FoldSub64
	/// </summary>
	public sealed class LoadSignExtend8x64FoldSub64 : BaseTransformation
	{
		public LoadSignExtend8x64FoldSub64() : base(IRInstruction.LoadSignExtend8x64)
		{
		}

		public override bool Match(Context context, TransformContext transformContext)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.Sub64)
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

			var e1 = transformContext.CreateConstant(Sub64(To64(t3), To64(t2)));

			context.SetInstruction(IRInstruction.LoadSignExtend8x64, result, t1, e1);
		}
	}
}
