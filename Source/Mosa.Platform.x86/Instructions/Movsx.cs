﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Compiler.Framework;
using Mosa.Compiler.Metadata;
using System;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// Representations the x86 Movsx instruction.
	/// </summary>
	public sealed class Movsx : X86Instruction
	{
		#region Data Members

		private static readonly OpCode R_X8 = new OpCode(new byte[] { 0x0F, 0xBE });
		private static readonly OpCode R_X16 = new OpCode(new byte[] { 0x0F, 0xBF });

		#endregion Data Members

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="Movsx"/>.
		/// </summary>
		public Movsx() :
			base(1, 1)
		{
		}

		#endregion Construction

		#region Methods

		/// <summary>
		/// Computes the opcode.
		/// </summary>
		/// <param name="destination">The destination operand.</param>
		/// <param name="source">The source operand.</param>
		/// <param name="third">The third operand.</param>
		/// <returns></returns>
		protected override OpCode ComputeOpCode(Operand destination, Operand source, Operand third)
		{
			if (!(destination.IsRegister))
				throw new ArgumentException(@"Destination must be RegisterOperand.", @"destination");
			if (source.IsConstant)
				throw new ArgumentException(@"Source must not be ConstantOperand.", @"source");

			switch (source.Type.Type)
			{
				case CilElementType.Boolean: goto case CilElementType.I1;
				case CilElementType.U1: goto case CilElementType.I1;
				case CilElementType.I1:
					{
						if ((destination.IsRegister) && (source.IsRegister)) return R_X8;
						if ((destination.IsRegister) && (source.IsMemoryAddress)) return R_X8;
					}
					break;

				case CilElementType.Char: goto case CilElementType.U2;
				case CilElementType.U2: goto case CilElementType.I2;
				case CilElementType.I2:
					if ((destination.IsRegister) && (source.IsRegister)) return R_X16;
					if ((destination.IsRegister) && (source.IsMemoryAddress)) return R_X16;
					break;

				default:
					break;
			}

			throw new ArgumentException(@"No opcode for operand type. [" + destination.GetType() + ", " + source.GetType() + ")");
		}

		/// <summary>
		/// Allows visitor based dispatch for this instruction object.
		/// </summary>
		/// <param name="visitor">The visitor object.</param>
		/// <param name="context">The context.</param>
		public override void Visit(IX86Visitor visitor, Context context)
		{
			visitor.Movsx(context);
		}

		#endregion Methods
	}
}