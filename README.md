# Project Framework and Portpolio

## Index

1. [Pattern](#pattern)
2. [Project Structure](#project-structure)
3. [UniRx](#unirx)
4. [Packages](#packages)

## Pattern

### 소개 
- 주로 사용하거나 프로젝트 사용시 필수로 사용되는 패턴

### 목적
- 유니티 이용한 게임 개발시 생산성을 높이고 유지 보수를 용이하게 하기 위함.

### 전략
- `다른 프로젝트들에서 개발한 기능들을 공용으로 사용`
    - 한번 구성한 뒤 재사용함으로서 편의 제공
    - 계속적인 리뷰를 통해 코드 리팩토링을 하고 더 수준높은 디자인 패턴 구현

- `ECS에 따른 객체지향적인 시스템을 만들것.`
    - 기존엔 `MVC`에 따른 코드를 지향했으나 `Unity`는 Entity(Gameobject)에 Component가 종속되어 있는 관계로
    MVC패턴을 적용하기가 까다롭다. 즉, 시스템의 속도 측면에서 ECS를 지향하는 방식으로 코드작업을 실행한다.
    (이것은 패턴뿐 아니라 일반적은 코드작업에서도 적용)

### 활용
-디자인 패턴은 컴포넌트처럼 되도록 어디에 쓰든 활용 가능하게.
-복잡할순 있어도 적용이 좋게(느슨한 결합)
<br>

## Project Structure

### 프로젝트 디렉토리 구조
- 상위 디렉토리는 씬 단위 (프로젝트 마다 달라질 수 있음) 로 Template 폴더를 복사하여 01부터 넘버링하여 사용 
- 하위 디렉토리는 기능 단위로 Template 폴더를 복사하여 사용

| 폴더명            | 역할 및 내용                                                            |
| ----------------- | ----------------------------------------------------------------------- |
| Template          | 씬 단위 디렉토리 트리를 구조화한 템플릿, 씬 추가시 복사해서 사용         |
| Template / 01. Scenes    | 해당 씬을 모은 폴더         |
| Template / 02. Script    | 스크립트를 기능 단위로 나누어서 폴더링  |
| Template / 02. Script / Template | 기능 단위 스크립트 폴더 템플릿, 기능 구현시 복사해서 사용  |
| Template / 03. Arts    | 리소스를 모은 폴더      |
| Template / 04. Documents    | 씬 단위 기능, 정의 등의 문서        |
| Template / 05. Prefabs    | 프리팹을 모은 폴더 |
| Template / 06. Packages    | 씬 단위로 사용하는 패키지 모음 |
| 01. Widebrain     | 와이드브레인 프레임워크 폴더                               |

## UniRx

### MVP Pattern

- Button
```C#
using Widebrain.UI;

//view
Button button;

//presenter
void Start()
{
    //case 1
    Reactive.Connect(button, () => Debug.Log("On Click"));
}
```

- InputFleid
```C#
using Widebrain.UI;

//view
InputFleid inputFleid;

//model
string str;
ReactiveProperty<string> r_str

//presenter
void Start()
{
    //case 1
    Reactive.Connect(inputFleid, str);

    //case 2
    Reactive.Connect(inputFleid, r_str);

    //case 3
    Reactive.Connect(inputField, (str) => Debug.Log(str) );
}
```

- ReactiveProperty
```C#
using Widebrain.UI;

//view
Text text;

//model
ReactiveProperty<string> r_str

//presenter
void Start()
{
    //case 1
    Reactive.Connect(r_str, text.text);
    
    //case 2
    Reactive.Connect(r_str, (str) => text.text = str);
}
```

<br>

## Packages
- [UniRx](https://assetstore.unity.com/packages/tools/integration/unirx-reactive-extensions-for-unity-17276)
- TextMesh Pro
- [ProceduralUIImage](https://assetstore.unity.com/packages/tools/gui/procedural-ui-image-52200)
- [PlatinioTween](https://assetstore.unity.com/packages/tools/animation/platiniotween-ui-animation-framework-152863)
- [File Browser PRO](https://assetstore.unity.com/packages/tools/utilities/file-browser-pro-98713)
- [DOTween Pro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416)
- [Runtime Preview Generator](https://assetstore.unity.com/packages/tools/camera/runtime-preview-generator-112860)
- PlayerPrefsExtension
