import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { UserService } from 'src/app/_service/user.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  // @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];


  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  // ngOnInit() {
  //   this.loadUser();
  // }

  // loadUser() {
  //   this.userService.getUser(+this.route.snapshot.params['id']).subscribe((user: User) =>
  //   {
  //     this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.galleryOptions = [
          {
            width: '500px',
            height: '500px',
            imagePercent: 100,
            thumbnailsColumns: 4,
            imageAnimation: NgxGalleryAnimation.Slide,
            preview: false
          }
        ];
        this.galleryImages = this.getImages();

  }

  // tslint:disable-next-line: typedef
  getImages() {
      const imageUrls = [];
      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < this.user.photos.length; i++) {
        imageUrls.push({
          small: this.user.photos[i].url,
          medium: this.user.photos[i].url,
          big: this.user.photos[i].url,
          description: this.user.photos[i].description
        });
      }
      return imageUrls;
    }

  //   this.route.queryParams.subscribe(params => {
  //     const selectedTab = params['tab'];
  //     this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
  //   });

  //   this.galleryOptions = [
  //     {
  //       width: '500px',
  //       height: '500px',
  //       imagePercent: 100,
  //       thumbnailsColumns: 4,
  //       imageAnimation: NgxGalleryAnimation.Slide,
  //       preview: false
  //     }
  //   ];
  //   this.galleryImages = this.getImages();
  // }

  // getImages() {
  //   const imageUrls = [];
  //   for (let i = 0; i < this.user.photos.length; i++) {
  //     imageUrls.push({
  //       small: this.user.photos[i].url,
  //       medium: this.user.photos[i].url,
  //       big: this.user.photos[i].url,
  //       description: this.user.photos[i].description
  //     });
  //   }
  //   return imageUrls;
  // }

  // selectTab(tabId: number) {
  //   this.memberTabs.tabs[tabId].active = true;
  // }


}
