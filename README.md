# UnityMiniGamePorject
 ## 1. 개발 기간 : 2024.09.25(수) ~ 2024.10.02(수)
 - 현재 업그레이드 및 수정 진행 중
 ## 2. 게임 장르 : 2D 플랫포머 게임
 ## 3. 게임 플랫폼: Window (PC)
 ## 4. 게임 진행 방식
 - 플레이어를 추적하는 몬스터를 회피 및 처치
 - 맵에 주어진 플랫폼(발판)을 통과하여 보스 몬스터 플랫폼에 도착
 - 보스 몬스터 토벌 시 게임 클리어
## 5. 주요 기능
### 5.1. 플레이어
```cs
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
 public enum PlayerState {Idle, Run, Jump, Die}
 public PlayerState curState;

 private void Update()
 {
  switch (curState)
  {
   case PlayerState.Idle:
    // Idle 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Run:
    // Run 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Jump:
    // Jump 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Die:
    // Die 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
  }
 }
}
```
- 플레이어는 총 4가지의 상태(Idle, Run, Jump, Die)가 존재
- 각 상태에 맞는 기능 및 애니메이션 출력 구현
- 상태 진행에서 특정 조건 달성 시 상태 변화
- 'L' 키를 입력하여 수리검을 투척하여 적 공격
- 적 오브젝트에게 피격 시 사망
### 5.2. 적 오브젝트 (일반)
```cs
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
 public enum EnemyState {Idle, Trace, Die}
 public EnemyState curState;

 private void Update()
 {
  switch (curState)
  {
   case PlayerState.Idle:
    // Idle 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Trace:
    // Trace 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Die:
    // Die 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
  }
 }
}
```
- 적 오브젝트 (일반)는 총 3가지의 상태(Idle, Trace, Die)가 존재
- 각 상태에 맞는 기능 및 애니메이션 출력 구현
- 상태 진행에서 특정 조건 달성 시 상태 변화
- 플레이어 오브젝트와 충돌 시 플레이어 오브젝트 파괴로 플레이어 사망 구현
- 플레이어에게 피격당해 체력에 해당하는 변수가 0이 되었을 때 사망
### 5.3. 적 오브젝트 (보스)
```cs
using UnityEngine;

public class BossController : MonoBehaviour 
{
 public enum BossState {Idle, Trace, Rush, Die}
 public EnemyState curState;

 private void Update()
 {
  switch (curState)
  {
   case PlayerState.Idle:
    // Idle 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Trace:
    // Trace 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Rush:
    // Rush 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
   case PlayerState.Die:
    // Die 상태에 맞는 기능 및 애니메이션 출력 함수
    break;
  }
 }
}
```
- 적 오브젝트 (보스)는 총 4가지의 상태(Idle, Trace, Rush, Die)가 존재
- 각 상태에 맞는 기능 구현 (애니메이션 추가 진행 중)
- 상태 진행에서 특정 조건 달성 시 상태 변화
- 플레이어 오브젝트를 추격하다 일정 시간을 주기로 플레이어에게 돌진 패턴의 공격 진행
- 플레이어 오브젝트와 충돌 시 플레이어 오브젝트 파괴로 플레이어 사망 구현
- 플레이어에게 피격당해 체력에 해당하는 변수가 0이 되었을 때 사망
