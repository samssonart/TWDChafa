// Copyright © Samssonart Games 2026

#include "Tower.h"
#include "Projectile.h"
#include "Kismet/GameplayStatics.h"

// Sets default values
ATower::ATower()
{
	PrimaryActorTick.bCanEverTick = true;
}

void ATower::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	_fireTimer += DeltaTime;

	if (_fireTimer < 1.0f / FireRate) return;

	TArray<AActor*> enemies;
	UGameplayStatics::GetAllActorsWithTag(GetWorld(),"Enemy", enemies);
	AActor* nearest = nullptr;
	float nearestDist = FLT_MAX;

	for (AActor* e : enemies)
	{
		float d = FVector::Dist(GetActorLocation(), e->GetActorLocation());
		if (d < nearestDist && d <= Range)
		{
			nearest = e;
			nearestDist = d;
		}
	}

	if (nearest)
	{
		FActorSpawnParameters SpawnParams;
		AActor* p = GetWorld()->SpawnActor<AActor>(ProjectileTemplate, _firePointInst->GetComponentLocation(), _firePointInst->GetComponentRotation(), SpawnParams);
		AProjectile* proj = Cast<AProjectile>(p);
		proj->Target = nearest;
		_fireTimer = 0.0f;
	}
}

void ATower::BeginPlay()
{
	Super::BeginPlay();
	
	if (!_firePointInst)
	{
		for (UActorComponent* Comp : GetComponents())
		{
			if (Comp && Comp->GetName() == TEXT("FirePoint"))
			{
				_firePointInst = Cast<USceneComponent>(Comp);
				break;
			}
		}
	}

	if (!_firePointInst)
	{
		UE_LOG(LogTemp, Warning, TEXT("FirePoint component not found on %s"), *GetName());
	}
}

