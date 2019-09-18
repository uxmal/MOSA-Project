﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework.IR;

namespace Mosa.Compiler.Framework.Transform.Manual.IR.Special
{
	public sealed class Phi64Invalid : BaseTransformation
	{
		public Phi64Invalid() : base(IRInstruction.Phi64)
		{
		}

		public override bool Match(Context context, TransformContext transformContext)
		{
			if (context.ResultCount == 0 || context.ResultCount > 2)
				return false;

			if (context.Operand1 != context.Result)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transformContext)
		{
			context.SetInstruction(IRInstruction.Nop);
		}
	}
}
