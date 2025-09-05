# Refatoração do ControlTankWarrior para State Machine

## O que foi feito

O código original do `ControlTankWarrior.cs` foi refatorado para usar o padrão **State Machine** (Máquina de Estados), mantendo todas as funcionalidades existentes mas organizando o código de forma mais escalável e maintível.

## Estrutura da Refatoração

### Arquivos Criados

1. **StateMachine/**
   - `IState.cs` - Interface que define o contrato para todos os estados
   - `TankStateMachine.cs` - Gerenciador da máquina de estados
   - `BaseState.cs` - Classe base com funcionalidades comuns a todos os estados

2. **StateMachine/States/**
   - `IdleState.cs` - Estado parado/idle
   - `WalkState.cs` - Estado caminhando
   - `RunState.cs` - Estado correndo
   - `JumpState.cs` - Estado pulando
   - `AttackState.cs` - Estado atacando

### Funcionalidades Mantidas

✅ **Movimento**: Andar e correr com diferentes velocidades
✅ **Rotação**: Rotação do personagem com input horizontal
✅ **Pulo**: Sistema de pulo com força e gravidade
✅ **Ataque**: Sistema de ataque com animações
✅ **Animações**: Todas as animações mantidas (`parado`, `andando`, `atacando`)
✅ **Eventos de Animação**: `OnAttackEnd()`, `OnWalkEnd()`, `animationHasEndend()`
✅ **Compatibilidade**: Propriedades legacy mantidas (`_playerController`, `_animator`)

## Vantagens da Refatoração

### 1. **Organização**
- Cada estado tem sua própria classe
- Responsabilidades bem definidas
- Código mais fácil de entender

### 2. **Escalabilidade**
- Fácil adicionar novos estados (ex: Dash, Slide, Climb)
- Cada estado é independente
- Transições bem definidas

### 3. **Manutenibilidade**
- Bugs isolados por estado
- Fácil de testar individualmente
- Modificações não afetam outros estados

### 4. **Flexibilidade**
- Estados podem ter comportamentos únicos
- Fácil customização de transições
- Suporte a estados complexos

## Como Adicionar Novos Estados

### Exemplo: Estado de Dash

1. Crie a classe do estado:
```csharp
public class DashState : BaseState
{
    private float dashDuration = 0.5f;
    private float dashSpeed = 15f;
    private float dashTimer;

    public DashState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        dashTimer = dashDuration;
        controller.moveDirection = controller.transform.forward * dashSpeed;
        // Configurar animação de dash
    }

    public override void Update()
    {
        dashTimer -= Time.deltaTime;
        
        if (dashTimer <= 0f)
        {
            controller.stateMachine.ChangeState(new IdleState(controller));
        }
        
        ApplyMovement();
    }
}
```

2. Adicione a transição nos outros estados:
```csharp
// No BaseState ou estados específicos
protected bool CheckTransitionToDash()
{
    if (Input.GetKeyDown(KeyCode.LeftControl))
    {
        controller.stateMachine.ChangeState(new DashState(controller));
        return true;
    }
    return false;
}
```

## Estrutura dos Estados

Cada estado implementa a interface `IState`:

- **`Enter()`**: Executado quando entra no estado
- **`Update()`**: Executado a cada frame
- **`Exit()`**: Executado quando sai do estado
- **`HandleInput()`**: Processa input e transições

## Transições de Estado

O sistema usa transições baseadas em prioridade:

1. **Ataque** (prioridade mais alta)
2. **Pulo**
3. **Movimento** (Walk/Run/Idle)

## Compatibilidade

O código refatorado mantém **100% de compatibilidade** com:
- Prefabs existentes
- Animation Controllers
- Scripts que referenciam o ControlTankWarrior
- Eventos de animação

## Comparação: Antes vs Depois

### Antes (Monolítico)
```csharp
void Update()
{
    ProcessInput();     // 40+ linhas
    ApplyMovement();    // Misturado com lógica
    UpdateAnimation();  // Estados hardcoded
}
```

### Depois (State Machine)
```csharp
void Update()
{
    stateMachine.Update(); // Delega para o estado atual
}
```

Cada estado agora tem:
- Responsabilidade única
- Lógica isolada
- Fácil de testar e modificar

## Próximos Passos Sugeridos

1. **Novos Estados**: Dash, Slide, Climb, Death
2. **Sub-Estados**: AttackCombo1, AttackCombo2, AttackCombo3
3. **Condições**: Estados condicionais (ex: OnlyIfGrounded)
4. **Dados Persistentes**: Sistema de dados entre estados
5. **Debugging**: Visual State Machine debugger no Inspector
