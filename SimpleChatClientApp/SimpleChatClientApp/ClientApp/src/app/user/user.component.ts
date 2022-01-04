import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from '../shared/models/user.model';
import { UserService } from './user.service';

@Component({
    selector: 'user',
    templateUrl: './user.component.html'
})
export class UserComponent {
    private readonly _userService: UserService;
    private readonly _router: Router;
    user: UserModel = new UserModel();

    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;
    }

    public createUser() {
        this._userService.createUser(this.user).subscribe(e => {
            localStorage.setItem('userId', e.id.toString());
            localStorage.setItem('nickname', e.nickname.toString());
            this._router.navigate(['/rooms']);
        });
    }
}
