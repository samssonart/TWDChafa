// Copyright © Samssonart Games 2026

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Tower.generated.h"

UCLASS()
class TDCHAFA_API ATower : public AActor
{
	GENERATED_BODY()
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category="Tower", meta=(AllowPrivateAccess="true"))
	float Range = 5.0f;
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category="Tower", meta=(AllowPrivateAccess="true"))
	float FireRate = 1.0f;
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category="Tower", meta=(AllowPrivateAccess="true"))
	TSubclassOf<AActor> ProjectileTemplate;
	
	USceneComponent* _firePointInst;
	
	float _fireTimer = 0.0f;
	
public:	
	ATower();
	virtual void Tick(float DeltaTime) override;
	virtual void BeginPlay() override;
	
};
