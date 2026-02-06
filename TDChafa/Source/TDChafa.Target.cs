// Copyright © Samssonart Games 2026

using UnrealBuildTool;
using System.Collections.Generic;

public class TDChafaTarget : TargetRules
{
	public TDChafaTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		DefaultBuildSettings = BuildSettingsVersion.V6;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_7;
		ExtraModuleNames.Add("TDChafa");
	}
}
