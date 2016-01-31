using UnityEngine;

public class Controller {

    public Controller()
    {

    }

    public virtual bool IsFirePressed()
    {
        return false;
    }

    public virtual bool IsSpecialPressed()
    {
        return false;
    }

	public virtual bool IsTrapPressed()
	{
		return false;
	}

    public virtual Vector2 Move()
    {
        return new Vector2();
    }

    public virtual Vector2 Aim()
    {
        return new Vector2();
    }

}
