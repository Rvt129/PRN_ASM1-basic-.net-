using Asm1.Bussiness;
using Asm1.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Asm1.Presentation.Admin
{
    /// <summary>
    /// Interaction logic for RoomList.xaml
    /// </summary>
    public partial class RoomList : System.Windows.Window
    {
        private RoomService roomService;
        private static RoomInformation selectedRoom;

        public RoomList()
        {
            InitializeComponent();
            lvi.ItemsSource = AllRoom();
            RoomTypeId.ItemsSource = AllType();
            RoomTypeId.DisplayMemberPath="RoomTypeName";
        }

        private IEnumerable<RoomInformation> AllRoom()
        {
            roomService = RoomService.Instance;
            return roomService.GetAllRoom();
        }
        private IEnumerable<RoomType> AllType()
        {
            roomService = RoomService.Instance;
            return roomService.GetAllType();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            PopupCreate_Room.IsOpen = true;
            RoomTy.ItemsSource = AllType();
            RoomTy.DisplayMemberPath = "RoomTypeName";
        }

        private void Update_Room(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                selectedRoom = button.CommandParameter as RoomInformation;
                if (selectedRoom != null)
                {
                    var tempRoom = new RoomInformation
                    {
                        RoomNumber = selectedRoom.RoomNumber,
                        RoomDetailDescription = selectedRoom.RoomDetailDescription,
                        RoomMaxCapacity = selectedRoom.RoomMaxCapacity,
                        RoomTypeId = selectedRoom.RoomTypeId,
                        RoomPricePerDay = selectedRoom.RoomPricePerDay,
                        RoomStatus = selectedRoom.RoomStatus
                    };
          
                    PopupUpdate_Room.DataContext = tempRoom;
                    var roomTypes = RoomTypeId.ItemsSource as List<RoomType>;
                    if (roomTypes != null)
                    {
                        var selectedType = roomTypes.FirstOrDefault(rt => rt.RoomTypeId == tempRoom.RoomTypeId);
                        RoomTypeId.SelectedItem = selectedType;
                    }
                    if (selectedRoom.RoomStatus == 0)
                    {
                        RoomStatus.SelectedItem = null;
                    }
                    else if (selectedRoom.RoomStatus == 1) { RoomStatus.SelectedValue = 1; }
                    else RoomStatus.SelectedValue = 2;
                    PopupUpdate_Room.IsOpen = true;
                }
            }
        }

        private void Delete_Room(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this room?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    var room = button.CommandParameter as RoomInformation;
                    if (room != null)
                    {
                        DeleteRoom(room.RoomId);
                    }
                }
            }
        }

        private void DeleteRoom(int roomId)
        {
            roomService = RoomService.Instance;
            roomService.DeleteRoom(roomId);
            lvi.ItemsSource=AllRoom();
        }

        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
            if (!search.Text.IsNullOrEmpty())
            {
                lvi.ItemsSource = SearchRoomList(search.Text);
            }
        }

        private IEnumerable<RoomInformation> SearchRoomList(string roomdetail)
        {
            roomService = RoomService.Instance;
            return roomService.SearchRoomByRoomDetail(roomdetail);
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void SaveUpdateRoomButton_Click(object sender, RoutedEventArgs e)
        {
           
            var tempRoom = PopupUpdate_Room.DataContext as RoomInformation;

            // Lấy giá trị từ các control
            string roomNumber = RoomNumber.Text;
            string roomDetail = RoomDetail.Text;
            int roomCapacity = int.Parse(RoomCapacity.Text);
            var selectedItem = (RoomType)RoomTypeId.SelectedValue;
            int roomTypeId = selectedItem.RoomTypeId;
            decimal roomPrice = decimal.Parse(RoomPrice.Text);
            byte status = 0;
            if (RoomStatus.SelectedValue != null)
            {

                if (byte.TryParse(RoomStatus.SelectedValue.ToString(), out byte parsedStatus))
                {
                    status = parsedStatus;
                }
            }
            else MessageBox.Show("You have to choose option");


            tempRoom.RoomNumber = roomNumber;
            tempRoom.RoomDetailDescription = roomDetail;
            tempRoom.RoomMaxCapacity = roomCapacity;
            tempRoom.RoomTypeId = roomTypeId;
            tempRoom.RoomPricePerDay = roomPrice;
            tempRoom.RoomStatus = status;

            selectedRoom.RoomNumber = tempRoom.RoomNumber;
            selectedRoom.RoomDetailDescription=tempRoom.RoomDetailDescription;
            selectedRoom.RoomMaxCapacity = tempRoom.RoomMaxCapacity;
            selectedRoom.RoomTypeId = tempRoom.RoomTypeId;
            selectedRoom.RoomPricePerDay = tempRoom.RoomPricePerDay;
            selectedRoom.RoomStatus = tempRoom.RoomStatus;

          
            PopupUpdate_Room.IsOpen = false;

            roomService = RoomService.Instance;
            roomService.UpdateRoom(selectedRoom);

   
            lvi.Items.Refresh();
        }



        private void SaveCreateRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RoomNum.Text) && !string.IsNullOrWhiteSpace(RoomDe.Text) && !string.IsNullOrWhiteSpace(RoomCap.Text) && !string.IsNullOrWhiteSpace(RoomTy.Text) && !string.IsNullOrWhiteSpace(RoomPri.Text))
            {
                var selectedItem = (RoomType)RoomTy.SelectedItem;
                int roomTypeId = selectedItem.RoomTypeId;
                RoomInformation room = new RoomInformation
                {
                    RoomNumber = RoomNum.Text,
                    RoomDetailDescription = RoomDe.Text,
                    RoomMaxCapacity = int.Parse(RoomCap.Text),
                    RoomTypeId = roomTypeId,
                    RoomPricePerDay = decimal.Parse(RoomPri.Text),
                    RoomStatus = 1 
                };
                roomService = RoomService.Instance;
                roomService.CreateRoom(room);
                lvi.ItemsSource = AllRoom();
                PopupCreate_Room.IsOpen = false;
            }
            else
            {
                MessageBox.Show("Incorrect input details", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
