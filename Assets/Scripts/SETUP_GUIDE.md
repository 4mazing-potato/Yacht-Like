# 1단계: 주사위 게임 설정 가이드

## Unity에서 설정하는 방법 (초보자용)

### Step 1: Canvas 만들기 (UI 컨테이너)

1. Hierarchy 창에서 우클릭
2. `UI` → `Canvas` 선택
3. Canvas가 자동으로 생성됨

### Step 2: GameManager 오브젝트 만들기

1. Hierarchy 창에서 우클릭
2. `Create Empty` 선택
3. 이름을 "GameManager"로 변경
4. Inspector에서 `Add Component` 클릭
5. `GameManager` 스크립트 추가

### Step 3: 주사위 5개 만들기

#### 주사위 1개 만들기:

1. Canvas를 우클릭 → `UI` → `Panel` 선택
2. 이름을 "Dice1"로 변경
3. Inspector에서 설정:
   - Width: 100
   - Height: 100
   - 색상: 원하는 색 (예: 파란색)

4. Dice1을 우클릭 → `UI` → `Text - TextMeshPro` 선택
   - 처음 사용 시 "Import TMP Essentials" 버튼 클릭
   - 이름을 "DiceText"로 변경
   - Inspector에서:
     - Text: "1"
     - Font Size: 60
     - 정렬: 중앙 (Horizontal/Vertical Center)
     - 색상: 흰색

5. Dice1 오브젝트 선택
6. `Add Component` → `DiceController` 스크립트 추가

#### 주사위 5개 복사:

7. Dice1을 선택하고 `Ctrl+D`로 복사
8. Dice2, Dice3, Dice4, Dice5 이름 변경
9. 5개를 화면 중앙에 일렬로 배치

### Step 4: 굴리기 버튼 만들기

1. Canvas 우클릭 → `UI` → `Button - TextMeshPro` 선택
2. 이름을 "RollButton"로 변경
3. 버튼 텍스트를 "주사위 굴리기"로 변경
4. 화면 하단에 배치

### Step 5: GameManager에 연결

1. GameManager 오브젝트 선택
2. Inspector의 GameManager 컴포넌트에서:
   - `Dice Controllers` 크기를 5로 설정
   - Dice1~5를 각각 드래그해서 Element 0~4에 할당

### Step 6: 테스트

1. Play 버튼 클릭
2. 스페이스바를 누르거나 버튼을 클릭해서 주사위 굴리기 테스트
3. Console 창에서 로그 확인

## 레이아웃 예시

```
┌─────────────────────────────────┐
│                                 │
│                                 │
│   [ 3 ] [ 5 ] [ 2 ] [ 4 ] [ 6 ]│  ← 주사위 5개
│                                 │
│                                 │
│     [ 주사위 굴리기 ]           │  ← 버튼
└─────────────────────────────────┘
```

## 동작 방법

- **스페이스바**: 주사위 굴리기
- **주사위 클릭**: 잠금/해제 (잠긴 주사위는 노란색)
- **3번 굴린 후**: "더 이상 굴릴 수 없습니다" 메시지

## 다음 단계

1단계가 완료되면:
- 2단계: 주사위 이미지로 변경
- 3단계: 애니메이션 추가
- 4단계: 점수 계산 시스템 연결

## 문제 해결

### TextMeshPro가 없다면?
- Window → TextMeshPro → Import TMP Essential Resources

### 주사위가 작동하지 않는다면?
- Console 창에서 에러 확인
- GameManager의 Dice Controllers가 올바르게 연결되었는지 확인

### 버튼이 작동하지 않는다면?
- 일단 스페이스바로 테스트 (Update 함수에 이미 구현됨)

