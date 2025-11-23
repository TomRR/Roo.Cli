using System.Diagnostics;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace ResultType.UnitTypes;

[DebuggerDisplay(nameof(NoErrors))]
[StructLayout(LayoutKind.Sequential, Size = 1)]
public readonly struct NoErrors : IStatusOnlyResult, Unit;

[DebuggerDisplay(nameof(Errors))]
[StructLayout(LayoutKind.Sequential, Size = 1)]
public readonly struct Errors : IStatusOnlyResult, Unit, IErrorResult;