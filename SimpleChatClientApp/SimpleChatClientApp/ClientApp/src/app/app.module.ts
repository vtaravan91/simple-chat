import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UserComponent } from './user/user.component';
import { CreateRoomComponent } from './create-room/create-room.component';
import { RoomsComponent } from './rooms/rooms.component';
import { MessageBoxComponent } from './message-box/message-box.component';
import { AuthGuard } from './shared/auth.guard';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        UserComponent,
        CreateRoomComponent,
        RoomsComponent,
        MessageBoxComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: 'user',
                component: UserComponent
            },
            {
                path: 'rooms',
                component: RoomsComponent,
                canActivate: [AuthGuard],
                children: [{
                    path: 'room/:roomId',
                    component: MessageBoxComponent,
                    canActivate: [AuthGuard]
                }]
            },
            { path: 'create-room', component: CreateRoomComponent, canActivate: [AuthGuard] },
            { path: '', redirectTo: '/user', pathMatch: 'full' }

        ])
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
