function viewModel() {
	var self = this;
	self.isLoggedIn = ko.observable(false);
	self.isManager = ko.observable(false);
	self.isHR = ko.observable(false);
	self.isUser = ko.observable(false);
	self.isEmployee = ko.observable(false);

	self.getUserInfo = function () {
		var userId = $("#userName").attr("user-id");
		if (userId == null || userId == undefined)
		{
			window.location.replace(appContext.buildUrl("Home/Login"));
		}
		$.ajax({
			type: "GET",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/User/GetRoleName/" + userId),
			success: function (data) {
				$("#userName").attr("role", data);
				if (data == "HR") {
					self.isHR(true);
				}
				else if (data == "Manager") {
					self.isManager(true);
				}
				else if (data == "Employee") {
					self.isEmployee(true);
				}
				else if (data == "User") {
					self.isUser(true);
					$('#calendar').calendar({
						enableContextMenu: false,
						enableRangeSelection: false,
						contextMenuItems: [
						],
						selectRange: function () {
						},
						mouseOnDay: function (e) {
							if (e.events.length > 0) {
								var content = '';

								for (var i in e.events) {
									content += '<div class="event-tooltip-content">'
										+ '<div class="event-fullname" style="color:' + e.events[i].color + '">' + e.events[i].fullname + '</div>'
										+ '<div class="event-type" style="color:' + e.events[i].color + '">' + e.events[i].type + '</div>'
										+ '</div>';
								}

								$(e.element).popover({
									trigger: 'manual',
									container: 'body',
									html: true,
									content: content
								});

								$(e.element).popover('show');
							}
						},
						mouseOutDay: function (e) {
							if (e.events.length > 0) {
								$(e.element).popover('hide');
							}
						},
						dayContextMenu: function () {
						},
						dataSource: [
						]
					});
				}
				else
				{
					self.isHR(false);
					self.isUser(false);
					self.isManager(false);
					self.isEmployee(false);
				}

				self.loadAllRequests();
			},
			error: function (data) {
			}
		});
		$.ajax({
			type: "GET",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/User/GetFullName/" + userId),
			success: function (data) {
				$("#userName").attr("fullname", data);
			},
			error: function (data) {
			}
		});
	}
	self.createUser = function () {
		var user = {
			FirstName: $("#firstname").val(),
			LastName: $("#lastname").val(),
			UserName: $("#username").val(),
			PassWord: $("#password").val(),
			Email: $("#email").val(),
			YearsOfService: $("#yearsOfService").val(),
			RoleId: $("#role").val()
		};

		$.ajax({
			type: "POST",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/HR/Create"),
			data: user,
			success: function (data) {
				$("#firstname").val('');
				$("#lastname").val('');
				$("#username").val('');
				$("#password").val('');
				$("#email").val('');
				$("#yearsOfService").val('');
				$("#role").val('');
			},
			error: function (data) {
			}
		});

	}
	self.setPolicy = function () {
		var policy = {
			MinYearsOfOffice: $("#minYearsOfOffice").val(),
			MaxYearsOfOffice: $("#maxYearsOfOffice").val(),
			PaidDayOffs: $("#paidDayOffs").val(),
			PaidSickness: $("#paidSickness").val(),
			UnPaidDayOffs: $("#unPaidDayOffs").val(),
			UnPaidSickness: $("#unPaidSickness").val()
		};
		$.ajax({
			type: "POST",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/HR/SetPolicy"),
			data: policy,
			success: function (data) {
				$("#minYearsOfOffice").val('');
				$("#maxYearsOfOffice").val('');
				$("#paidDayOffs").val('');
				$("#paidSickness").val('');
				$("#unPaidDayOffs").val('');
				$("#unPaidSickness").val('');
			},
			error: function (data) {
			}
		});
	};
	self.setHoliday = function () {
		var holiday = {
			Name: $("#nameOfHoliday").val(),
			Date: $('#dateOfHoliday').datepicker('getDate'),
			HRId: $("#userName").attr("user-id")
		};
		$.ajax({
			type: "POST",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/HR/SetCompanyHoliday"),
			data: holiday,
			success: function (data) {
				$("#nameOfHoliday").val('');
			},
			error: function (data) {
			}
		});
	};
	self.loadAllRequests = function () {
		if (self.isEmployee() == true) {
			var userId = $("#userName").attr("user-id");
			$.ajax({
				type: "GET",
				beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
				url: appContext.buildUrl("api/Vacation/GetAllById/" + userId),
				success: function (data) {
					for (var i in data) {
						data[i].EndDate = data[i].EndDate.substring(0, data[i].EndDate.indexOf('T')).split('-').map(Number);
						data[i].StartDate = data[i].StartDate.substring(0, data[i].StartDate.indexOf('T')).split('-').map(Number);
					}
					var dataSource = $('#calendar').data('calendar').getDataSource();
					for (var i in data) {
						var event;
						if (data[i].Status == "InQueue") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "#808080"
							};
						}
						else if (data[i].Status == "Approved") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "green"
							};
						}
						else if (data[i].Status == "Declined") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "red"
							};
						}
						dataSource.push(event);
						event = null;
					}
					$('#calendar').data('calendar').setDataSource(dataSource);
				},
				error: function (data) {
				}
			});
		}
		else {
			$.ajax({
				type: "GET",
				beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
				url: appContext.buildUrl("api/Vacation/GetAll"),
				success: function (data) {
					for (var i in data) {
						data[i].EndDate = data[i].EndDate.substring(0, data[i].EndDate.indexOf('T')).split('-').map(Number);
						data[i].StartDate = data[i].StartDate.substring(0, data[i].StartDate.indexOf('T')).split('-').map(Number);
					}
					var dataSource = $('#calendar').data('calendar').getDataSource();
					for (var i in data) {
						var event;
						if (data[i].Status == "InQueue") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "#808080"
							};
						}
						else if (data[i].Status == "Approved") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "green"
							};
						}
						else if (data[i].Status == "Declined") {
							event = {
								id: data[i].Id,
								fullname: data[i].FullName,
								type: data[i].VacationType,
								startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
								endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
								color: "red"
							};
						}
						dataSource.push(event);
						event = null;
					}
					$('#calendar').data('calendar').setDataSource(dataSource);
				},
				error: function (data) {
				}
			});
		}

	};

	self.loadAllHolidays = function ()
	{
		$.ajax({
			type: "GET",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/Holiday/GetAll"),
			success: function (data) {
				for (var i in data) {
					data[i].EndDate = data[i].EndDate.substring(0, data[i].EndDate.indexOf('T')).split('-').map(Number);
					data[i].StartDate = data[i].StartDate.substring(0, data[i].StartDate.indexOf('T')).split('-').map(Number);
				}
				var dataSource = $('#calendar').data('calendar').getDataSource();
				for (var i in data) {
					var event = {
						id: data[i].Id,
						fullname: data[i].Name,
						type: "Holiday",
						startDate: new Date(data[i].StartDate[0], data[i].StartDate[1] - 1, data[i].StartDate[2]),
						endDate: new Date(data[i].EndDate[0], data[i].EndDate[1] - 1, data[i].EndDate[2]),
						color: "blue"
					};
					dataSource.push(event);
					event = null;
				}
				$('#calendar').data('calendar').setDataSource(dataSource);



			},
			error: function (data) {
			}
		});
	};

	self.approveRequest = function (){
		var requestid = $('#event-modal input[name="event-index"]').val();
		var userId = $("#userName").attr("user-id");
		$.ajax({
			type: "PUT",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/Manager/Approve/" + requestid + "/" + userId),
			success: function (data) {
				var dataSource = $('#calendar').data('calendar').getDataSource();

				for (var i in dataSource) {
					if (dataSource[i].id == requestid) {
						dataSource[i].color = "green";
						break;
					}
				}
				$('#calendar').data('calendar').setDataSource(dataSource);
				$('#event-modal').modal('hide');

			},
			error: function (data) {
			}
		});
	}

	self.declineRequest = function () {

		var requestid = $('#event-modal input[name="event-index"]').val();
		var userId = $("#userName").attr("user-id");
		$.ajax({
			type: "PUT",
			beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
			url: appContext.buildUrl("api/Manager/Decline/" + requestid + "/" + userId),
			success: function (data) {
				var dataSource = $('#calendar').data('calendar').getDataSource();

				for (var i in dataSource) {
					if (dataSource[i].id == requestid) {
						dataSource[i].color = "red";
						break;
					}
				}

				$('#calendar').data('calendar').setDataSource(dataSource);
				$('#event-modal').modal('hide');
			},
			error: function (data) {
			}
		});

	}
};



function editEvent(event) {
	$('#event-modal input[name="event-index"]').val(event ? event.id : '');
	$('#event-modal input[name="event-fullname"]').val(event ? event.fullname : '');
	$('#event-modal input[name="event-type"]').val(event ? event.type : '');
	$('#event-modal input[name="event-start-date"]').datepicker('update', event ? event.startDate : '');
	$('#event-modal input[name="event-end-date"]').datepicker('update', event ? event.endDate : '');
	$('#event-modal').modal();
}

function deleteEvent(event) {
	var dataSource = $('#calendar').data('calendar').getDataSource();
	$.ajax({
		type: "DELETE",
		beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
		url: ("api/Employee/Remove/"+event.id),
		success: function (data) {

		},
		error: function (data) {
		}
	});

	for (var i in dataSource) {
		if (dataSource[i].id == event.id) {
			dataSource.splice(i, 1);
			break;
		}
	}

	$('#calendar').data('calendar').setDataSource(dataSource);
}

function saveEvent() {
	var event = {
		id: $('#event-modal input[name="event-index"]').val(),
		type: $('#event-modal input[name="event-type"]').val(),
		startDate: $('#event-modal input[name="event-start-date"]').datepicker('getDate'),
		endDate: $('#event-modal input[name="event-end-date"]').datepicker('getDate')
	}
	var request = {
		VacationType: $('#event-modal input[name="event-type"]').val(),
		StartDate: $('#event-modal input[name="event-start-date"]').datepicker('getDate'),
		EndDate: $('#event-modal input[name="event-end-date"]').datepicker('getDate'),
		UserId: $("#userName").attr("user-id"),
		Id: $('#event-modal input[name="event-index"]').val()
	};
	$.ajax({
		type: "POST",
		beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", "Bearer " + appContext.token); },
		data: request,
		url: appContext.buildUrl("api/Employee/Create"),
		success: function (data) {
			event.fullname = data;
			var dataSource = $('#calendar').data('calendar').getDataSource();

			if (event.id) {
				for (var i in dataSource) {
					if (dataSource[i].id == event.id) {
						dataSource[i].fullname = event.fullname;
						dataSource[i].startDate = event.startDate;
						dataSource[i].endDate = event.endDate;
					}
				}
			}
			else {
				var newId = 0;
				for (var i in dataSource) {
					if (dataSource[i].id > newId) {
						newId = dataSource[i].id;
					}
				}

				newId++;
				event.id = newId;
				event.color = "#808080";

				dataSource.push(event);
			}

			$('#calendar').data('calendar').setDataSource(dataSource);
			$('#event-modal').modal('hide');
		},
		error: function (data) {
			$('#event-modal').modal('hide');
		}
	});
}

$(function () {
	$('#calendar').calendar();
	$('#calendar').calendar({
		enableContextMenu: true,
		enableRangeSelection: true,
		contextMenuItems: [
			{
				text: 'Update',
				click: editEvent
			},
			{
				text: 'Delete',
				click: deleteEvent
			}

		],
		selectRange: function (e) {
			editEvent({ startDate: e.startDate, endDate: e.endDate });
		},
		mouseOnDay: function (e) {
			if (e.events.length > 0) {
				var content = '';

				for (var i in e.events) {
					content += '<div class="event-tooltip-content">'
						+ '<div class="event-fullname" style="color:' + e.events[i].color + '">' + e.events[i].fullname + '</div>'
						+ '<div class="event-type" style="color:' + e.events[i].color + '">' + e.events[i].type + '</div>'
						+ '</div>';
				}

				$(e.element).popover({
					trigger: 'manual',
					container: 'body',
					html: true,
					content: content
				});

				$(e.element).popover('show');
			}
		},
		mouseOutDay: function (e) {
			if (e.events.length > 0) {
				$(e.element).popover('hide');
			}
		},
		dayContextMenu: function (e) {
			$(e.element).popover('hide');
		},
		dataSource: [
		]
	});
	var vm = new viewModel();
	vm.getUserInfo();
	vm.loadAllHolidays();
	ko.applyBindings(vm, $(".vacationCalendar")[0]);

	$("#dateOfHoliday").datepicker();

	var currentYear = new Date().getFullYear();


	$('#save-event').click(function () {
		saveEvent();
	});
});