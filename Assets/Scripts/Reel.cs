public class Reel {

    private int id;

    public Reel(int id)
    {
        this.id = id;
        SlotMachine.On_StateChanged += SlotMachineStateChange;
    }

    ~Reel()
    {
        SlotMachine.On_StateChanged -= SlotMachineStateChange;
    }

    private void SlotMachineStateChange(SlotMachineState _state, Combo _currentCombo)
    {
        if(_state == SlotMachineState.Rolling)
        {
            ReelView.On_ReelViewSpinning?.Invoke(id, _currentCombo);
        }
    }
}