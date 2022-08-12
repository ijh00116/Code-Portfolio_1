# Project Framework and Portpolio

## Index

1. [Pattern](#pattern)
2. [적용된패턴](#adaptedpatterns)
    - [옵저버패턴](#observerpattern)
    - 
2. [Project Structure](#project-structure)

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
- 디자인 패턴은 컴포넌트처럼 되도록 어디에 쓰든 활용 가능하게.
- 복잡할순 있어도 적용이 좋게(느슨한 결합)

## AdaptedPatterns

### Observerpattern
- 특정 클래스 등의 정보를 전달하기 위해 직접 간섭이 아니라 정보를 전달함으로써 클래스 간의 심한 간섭을 없앤다.
- 전달받기위해 등록된 함수들을 관리한다.

```code
private static Dictionary<string, List<Delegate>> handlers = new Dictionary<string, List<Delegate>>();

    private static void RegisterListener(string messageName, Delegate callback)
    {
        if (callback == null)
            return;
        if(!handlers.ContainsKey(messageName))
        {
            handlers.Add(messageName, new List<Delegate>());
        }
        List<Delegate> messagelst = handlers[messageName];
        Delegate ms = messagelst.Find(o => o.Method == callback.Method && o.Target == callback.Target);
        if(ms!=null)
        {
            throw new ArgumentException("Callback method is already exist!!", messageName);
        }
        messagelst.Add(callback);
    }
    private static void UnRegisterListener(string messageName, Delegate callback)
    {
        if (callback == null)
            return;
        if (!handlers.ContainsKey(messageName))
            return;

        List<Delegate> messagelst = handlers[messageName];
        Delegate ms = messagelst.Find(o => o.Method == callback.Method && o.Target == callback.Target);
        if (ms == null)
            return;
        messagelst.Remove(ms);
    }

    private static void SendMessage<T>(string messageName,T e) where T:Message
    {
        if (!handlers.ContainsKey(messageName))
            return;

        List<Delegate> messagelst = handlers[messageName];

        for(int i=0; i<messagelst.Count; i++)
        {
            if (messagelst[i].GetType() != typeof(Action<T>))
                continue;

            var _event=(Action<T>)messagelst[i];
            _event(e);
        }
    }
```









## Project Structure
