// Copyright © Samssonart Games 2026

using UnrealBuildTool;
using System.Collections.Generic;

public class TDChafaEditorTarget : TargetRules
{
	public TDChafaEditorTarget( TargetInfo Target) : base(Target)
	{
		Type = TargetType.Editor;
		DefaultBuildSettings = BuildSettingsVersion.V6;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_7;
		ExtraModuleNames.Add("TDChafa");
	}
}
