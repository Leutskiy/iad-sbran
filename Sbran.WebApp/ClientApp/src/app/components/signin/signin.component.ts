import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HomePage, News, Vote, VoteList } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {

  errorMessage: string;
  username: string;
  password: string;
  mainDatas: HomePage;
  news: News;
  vote: Vote;
  voteList: VoteList;
  voteLists: VoteList[] = [];
  newsL: News[] = [];
  voteL1: VoteList;
  voteL2: VoteList;
  voteL3: VoteList;
  voteL4: VoteList;

  constructor(
    private router: Router,
    private authService: AuthService) {
    this.mainDatas = new HomePage();
    this.news = new News();
    this.vote = new Vote();
    this.vote.voteLists = [];
    this.voteList = new VoteList();
    this.voteL1 = new VoteList();
    this.voteL2 = new VoteList();
    this.voteL3 = new VoteList();
    this.voteL4 = new VoteList();
    this.username = "";
    this.password = "";
  }

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.authService.get().subscribe(e => {
      console.log(e);
      this.mainDatas = JSON.parse(JSON.stringify(e));
    })
  }

  sendNews() {
    console.log("qwewqe");
    this.authService.sendNews(this.news).subscribe(e => {
      this.newsL.push(this.news);
      this.mainDatas.news.push(this.news);
      this.news = new News();
      this.getAll();
    })
  }

  sendVoteList() {
    this.voteLists = [];
    if (this.voteL1.name != null) {
      this.vote.voteLists.push(this.voteL1);
    }
    if (this.voteL2.name != null) {
      this.vote.voteLists.push(this.voteL2);
    }
    if (this.voteL3.name != null) {
      this.vote.voteLists.push(this.voteL3);
    }
    if (this.voteL4.name != null) {
      this.vote.voteLists.push(this.voteL4);
    }
    this.authService.sendVote(this.vote).subscribe(e => {
      console.log("success");
      this.voteL1 = new VoteList();
      this.voteL2 = new VoteList();
      this.voteL3 = new VoteList();
      this.voteL4 = new VoteList();
      this.vote = new Vote();
      this.getAll();
    })
  }

  sendVoteListCount(id: string) {
    this.authService.sendVoteList(id).subscribe(e => {
      this.getAll();
    })
  }

  login() {
    this.authService.login(this.username, this.password).subscribe(
      accountDetailsResult => {
        this.router.navigateByUrl(`/profile/${accountDetailsResult.profileId}/employee/${accountDetailsResult.employeeId}`);
      },
      error => {
        console.log(error);
        this.errorMessage = error.error_description;
      }
    );
  }
}
