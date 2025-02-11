
//Used to simulate mono
public interface Executor
{       
        //gets called every frame for world space
        void Update(Gamedata gamedata);

        //gets called every frame for screen space
        void LateUpdate(Gamedata gamedata);
}

