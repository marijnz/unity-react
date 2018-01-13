package com.marijnzwemmer.unityreact;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

import com.facebook.react.ReactRootView;
import com.unity3d.player.UnityPlayer;

/**
 * Created by marijnzwemmer on 11/11/17.
 */

public class ContainerLayout extends RelativeLayout {
    final static int transparant = Color.parseColor("#00000000");

    UnityPlayer unityPlayer;
    ReactRootView reactRootView;
    Context context;

    public ContainerLayout(Context context, final UnityPlayer unityPlayer, final ReactRootView reactRootView) {
        super(context);
        this.unityPlayer = unityPlayer;
        this.reactRootView = reactRootView;
        this.context = context;

        final RelativeLayout subLayout1 = createLayout();
        final RelativeLayout subLayout2 = createLayout();

        subLayout1.setBackground(new ColorDrawable(transparant));
        subLayout2.setBackground(new ColorDrawable(transparant));

        subLayout1.addView(unityPlayer);
        subLayout2.addView(reactRootView);

        addView(subLayout1);
        addView(subLayout2);
    }

    @Override
    public boolean onInterceptTouchEvent(MotionEvent ev) {
        // Assuming that React is on top Unity, first let React handle touches as usual, then manually Unity
        onTouchEvent(ev);
        unityPlayer.onTouchEvent(ev);
        return false;
    }

    private RelativeLayout createLayout() {
        RelativeLayout layout = new RelativeLayout(context);
        layout.setLayoutParams(new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT));
        layout.setLayoutDirection(View.LAYOUT_DIRECTION_LTR);
        return layout;
    }
}
