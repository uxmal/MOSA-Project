﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework.IR;

namespace Mosa.Compiler.Framework.Transform.Manual.IR.Rewrite
{
	public sealed class Compare32x32Combine32x64 : BaseTransformation
	{
		public Compare32x32Combine32x64() : base(IRInstruction.Compare32x32)
		{
		}

		public override bool Match(Context context, TransformContext transformContext)
		{
			if (context.ConditionCode != ConditionCode.Equal && context.ConditionCode != ConditionCode.NotEqual)
				return false;

			if (IsResolvedConstant(context.Operand1))
				return false;

			if (!IsResolvedConstant(context.Operand2))
				return false;

			if (context.Operand2.ConstantUnsigned32 != 0)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.Compare32x64)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transformContext)
		{
			var node2 = context.Operand1.Definitions[0];
			var conditionCode = context.ConditionCode == ConditionCode.NotEqual ? node2.ConditionCode : node2.ConditionCode.GetOpposite();

			context.SetInstruction(IRInstruction.Compare32x32, conditionCode, context.Result, node2.Operand1, node2.Operand2);
		}
	}
}
